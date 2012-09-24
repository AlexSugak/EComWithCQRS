/* 
* Copyright (c) 2012 Leonardo Cardoso (http://leocardz.com)
* Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
* and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
*
* Version: 0.4.47
* 
*/
(function ($) {
	$.fn.linkToProductPreview = function (options) {

		var settings = $.extend(true, {
			url: '',
			loaderImage: '',
			productImageContainer: '',
			productImageHidden: ''
		}, options);

		function trim(str) {
			return str.replace(/^\s+|\s+$/g, "");
		}

		var urlRegex = /(https?\:\/\/|\s)[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})(\/+[a-z0-9_.\:\;-]*)*(\?[\&\%\|\+a-z0-9_=,\.\:\;-]*)?([\&\%\|\+&a-z0-9_=,\:\;\.-]*)([\!\#\/\&\%\|\+a-z0-9_=,\:\;\.-]*)}*/i;
		var block = false;

		$(this).focus(function () {
		});
		$(this).blur(function () {
		});

		$(this).keyup(function (e) {
			if ((e.which == 13 || e.which == 32 || e.which == 17) && trim($(this).val()) != "") {
				var text = " " + $(this).val();
				if (block == false && urlRegex.test(text)) {
					block = true;
					$('#divFailedToParse').remove();
					$(this).after('<div id="divImageLoader"><img id="imgLoader" src="' + settings.loaderImage + '"/></div>');

					var thisId = $(this).attr('id');

					$.ajax({
						url: settings.url,
						type: 'GET',
						data: { productUrl: text },
						dataType: 'json',
						success: function (result) {
							block = false;
							$('#divImageLoader').remove();

							if (result.parsed) {
								$('#Name').val(result.name);
								$('#Description').val(result.description);
								$('#Price').val(result.price);

								if (result.image != '' && result.image != null) {
									$('#' + settings.productImageHidden).val(result.image);
									$('#' + settings.productImageContainer).html('<img src="' + result.image + '"/>');
								}
							}
							else {
								clearData();
								failedParsingProductPage(thisId);
							}
						},
						error: function (xhr, ajaxOptions, thrownError) {
							failedParsingProductPage(thisId);
							block = false;
							$('#divImageLoader').remove();
							clearData();
						}
					});
				}
			}
		});

		function clearData() {
			$('#Name').val('');
			$('#Description').val('');
			$('#Price').val('');
			
			$('#' + settings.productImageHidden).val('');
			$('#' + settings.productImageContainer).html('');
		}

		function failedParsingProductPage(inputId) {
			$('#' + inputId).after('<div id="divFailedToParse">Cannot load product details, please enter the information below by hand.</div>');
		}
	}
})(jQuery);