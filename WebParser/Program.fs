// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
// #r "../../../bin/FSharp.Data.dll"
open FSharp.Data
open System

module WebParser =

    type Page = 
        { url : string
          content : HtmlDocument
          pureText : string }

    let GetPureText (page: HtmlDocument) : string =
        page.Descendants["Body"] 
            |> Seq.collect(fun x -> x.InnerText()) 
            |> Array.ofSeq
            |> String.Concat

    let LoadPage (url: string) =
        let content = HtmlDocument.Load(url)
        {url = url;
        content = content;
        pureText = GetPureText(content)}

