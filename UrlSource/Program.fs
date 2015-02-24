// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open FSharp.Data

module UrlSource =

    let GetGoogleLinks term=
        HtmlDocument.Load("http://www.google.co.uk/search?q="+term).Descendants ["a"]
            |> Seq.choose (fun x -> 
                   x.TryGetAttribute("href")
                   |> Option.map (fun a -> x.InnerText(), a.Value()))
            |> Seq.filter (fun (name, url) -> 
                        name <> "Cached" && name <> "Similar" && url.StartsWith("/url?"))
            |> Seq.map (fun (_, url) -> url.Substring(0, url.IndexOf("&sa=")).Replace("/url?q=", ""))
            |> Seq.filter (fun (url) -> url.StartsWith("http"))
            |> Seq.toArray

