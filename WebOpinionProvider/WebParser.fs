module WebParser 

open FSharp.Data
open System

type Page = 
    { url : string
      pureText : string }

let GetPureText (page: HtmlDocument) : string =
    page.Descendants["Body"] 
        |> Seq.collect(fun x -> x.InnerText()) 
        |> Array.ofSeq
        |> String.Concat

let LoadPage (url: string) =
    try
        {url = url;
        pureText = GetPureText(HtmlDocument.Load(url))}
    with
        |_ ->
            {url = url;
            pureText = ""}

