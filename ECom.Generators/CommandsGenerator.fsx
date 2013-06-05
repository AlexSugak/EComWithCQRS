﻿// 
// This is the script generating Commands classes
// 
//  In order to add, modify, delete commadn do the following:
//  1. Scroll to the end of the file, there're definitions of the commands
//  2. add, modify, delete the command definition
//      Note: commands should be in the following format:
//          command "<commandName>" [<agrument separated by semicolon(;)>]
//      where each argument is either:
//          <typeAlias> "<argumentName>"
//      or
//          arg "<argumentName>" typeof<argumentType>
//
//      Example:
//          command "RegisterUser" [string "Email"; string "Password"; int "Age"]
//      the same command
//          command "RegisterUser" [arg "Email" typeof<string>; arg "Password" typeof<string>; arg "Age" typeof<int>]
//
//      When the modification of definitions is done, you need to generate classes. For that
//      1. Select all text in this file (Ctrl+A)
//      2. Press Alt+Enter
//      3. In Visual studio new windows will be opened "F# Interactive". There will be the result of scrit execution. In the context menu of that windows call "Reset interaction session" menu item to release the dll
//

#if INTERACTIVE
#r "bin\Debug\ECom.Messages.dll"
#endif

open System
open System.IO
open ECom.Messages

//===================================================

let stamp = String.Format("//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the \"{0}\" script.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
", __SOURCE_FILE__)

type argument =
    {
        Name: string
        Type: Type
    }   

let toCamel (value:string) = String.Format("{0}{1}", value.[0].ToString().ToLower(), value.Substring(1))

let arg (argName:string) (argType:Type) = {argument.Name = argName; Type = argType}

let fieldTempate = "
    [DataMember(Name = \"{0}\")]
    private {1} {2};

    public {1} {0}
    {{
        get {{ return this.{2}; }}
    }}
"
let ctorParams = "{1} {0}"
let ctorAssignment = "
        this.{0} = {0};"

let idProp = "
    public {1} Id
    {{
        get {{ return this.{0}; }}
    }}
"
let valueHashLineTemplate = "
        hash = hash * 29 + this.{0}.GetHashCode();"

let referenceHashLineTemplate = "
        if (this.{0} != null)
            hash = hash * 29 + this.{0}.GetHashCode();"

let commandRecordTemplate = "
[GeneratedCodeAttribute(\"CommandsGenerator.fsx\", \"1.0.0.0\")]
[DataContract(Namespace = \"http://teamboard.com/contracts/commands/\")]
public sealed class {0}: ICommand<{6}>, IEquatable<{0}>
{{{7}{1}
    public {0}({2})
    {{{3}
    }}
    
    public bool Equals({0} other)
    {{
        if (this != null)
		{{
			return other != null && {4};
		}}
		return other == null;
    }}

    public override bool Equals(object obj)
    {{
        var other = obj as {0};
        return other != null && this.Equals(other);
    }}

    public override int GetHashCode()
    {{
        var hash = 17;
        {5}
        
        return hash;
    }}

    public static bool operator ==({0} a, {0} b)
    {{
	    if (System.Object.ReferenceEquals(a, b))
		{{
			return true;
		}}
		if (((object)a == null) || ((object)b == null))
		{{
			return false;
		}}
		return a.Equals(b);
	}}

	public static bool operator !=({0} a, {0} b)
	{{
		return !(a == b);
	}}
}}
"

let funcCommandRecordTemplate = "
[GeneratedCodeAttribute(\"CommandsGenerator.fsx\", \"1.0.0.0\")]
[DataContract(Namespace = \"http://teamboard.com/contracts/commands/\")]
public sealed class {0}: IFunctionalCommand, IEquatable<{0}>
{{{1}
    public {0}({2})
    {{{3}
    }}
    
    public bool Equals({0} other)
    {{
        if (this != null)
		{{
			return other != null && {4};
		}}
		return other == null;
    }}

    public override bool Equals(object obj)
    {{
        var other = obj as {0};
        return other != null && this.Equals(other);
    }}

    public override int GetHashCode()
    {{
        var hash = 17;
        {5}
        
        return hash;
    }}

    public static bool operator ==({0} a, {0} b)
    {{
	    if (System.Object.ReferenceEquals(a, b))
		{{
			return true;
		}}
		if (((object)a == null) || ((object)b == null))
		{{
			return false;
		}}
		return a.Equals(b);
	}}

	public static bool operator !=({0} a, {0} b)
	{{
		return !(a == b);
	}}
}}
"

let typeName (typeObj: Type) = 
    match typeObj.IsGenericType with
    | true -> String.Format("{0}<{1}>", typeObj.Name.Remove(typeObj.Name.IndexOf('`')), String.Join(", ", typeObj.GenericTypeArguments |> Array.map (fun x -> x.Name)))
    | false -> typeObj.Name

let equalityExpression args = args |> List.choose (fun x -> Some(String.Format("{0}.Equals(this.{1}, other.{1})", typeName x.Type, x.Name))) |> List.reduce (fun x y -> x + " && " + y)
let hashGenerator args = args |> List.choose (fun x -> Some(if (x.Type.IsValueType) then String.Format(valueHashLineTemplate, x.Name) else String.Format(referenceHashLineTemplate, x.Name))) |> List.reduce (fun x y -> x + y)

let record (recordTemplate:string) (name:string) args (id:argument) =
    String.Format(recordTemplate, name, args |> List.map (fun x -> String.Format(fieldTempate, x.Name, typeName x.Type, toCamel x.Name)) |> List.reduce (fun x y -> x + y), args |> List.map (fun x -> String.Format(ctorParams, toCamel x.Name, typeName x.Type)) |> List.reduce (fun x y -> String.Format("{0}, {1}", x, y)), args |> List.map (fun x -> String.Format(ctorAssignment, toCamel x.Name)) |> List.reduce (fun x y -> x + y), equalityExpression args, hashGenerator args, typeName id.Type, String.Format(idProp, toCamel id.Name, typeName id.Type))

let funcRecord (recordTemplate:string) (name:string) args =
    String.Format(recordTemplate, name, args |> List.map (fun x -> String.Format(fieldTempate, x.Name, typeName x.Type, toCamel x.Name)) |> List.reduce (fun x y -> x + y), args |> List.map (fun x -> String.Format(ctorParams, toCamel x.Name, typeName x.Type)) |> List.reduce (fun x y -> String.Format("{0}, {1}", x, y)), args |> List.map (fun x -> String.Format(ctorAssignment, toCamel x.Name)) |> List.reduce (fun x y -> x + y), equalityExpression args, hashGenerator args)


let Generate elements = File.WriteAllText (Path.Combine(__SOURCE_DIRECTORY__, "..\ECom.Messages\Commands.cs"), List.reduce (fun x y -> x + y) (stamp::elements))

let command name args = record commandRecordTemplate name args args.Head
let funcCommand name args = funcRecord funcCommandRecordTemplate name args

//===================================================

let header = "
using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace ECom.Messages
{
"

let footer = "
}
"

// Type aliases
let guid name = arg name typeof<Guid>
let string name = arg name typeof<string>
let int name = arg name typeof<int>
let byte name = arg name typeof<byte>
let bool name = arg name typeof<bool>
let datetime name = arg name typeof<DateTime>
let date name = arg name typeof<DateTime>
let decimal name = arg name typeof<decimal>
let id name = arg name typeof<Guid>
let uri name = arg name typeof<Uri>

// Field aliases
let version = arg "OriginalVersion" typeof<int>
let userId = arg "UserId" typeof<UserId>
let orderId = arg "OrderId" typeof<OrderId>
let email = arg "Email" typeof<EmailAddress>
let orderItemId = arg "OrderItemId" typeof<OrderItemId>
let productId = arg "ProductId" typeof<ProductId>


// Commands
let commands = [
                header

                //
                // Public
                //
                command "ReportUserLoggedIn"        [userId; string "UserName"; string "PhotoUrl"]
                command "SetUserEmail"              [userId; email; version]
                command "CreateNewOrder"            [orderId; userId]
                command "AddProductToOrder"         [orderId; orderItemId; uri "ProductUri"; string "Name"; string "Description"; decimal "Price"; int "Quantity"; string "Size"; string "Color"; uri "ImageUri"; version]
                command "RemoveItemFromOrder"       [orderId; orderItemId; version]
                command "SubmitOrder"               [orderId; version]
               
                footer
            ]
Generate (commands)