module UrlSource 

open FSharp.Data
open System


let GetGoogleSearchUrl (term, fromDate: Option<DateTime>, toDate: Option<DateTime>) =
    let result = "http://www.google.co.uk/search?q=" + term+ "&num=10&lr=lang_en"
    let mutable datePart = 
        match fromDate with
        | Some(date) -> "%2Ccd_min%3A" + date.ToString("dd.MM.yyyy")
        | None -> String.Empty

    datePart <- datePart + 
        match toDate with
        | Some(date) -> "%2Ccd_max%3A" + date.ToString("dd.MM.yyyy")
        | None -> String.Empty
        
    datePart <- 
        match datePart with
        | something when something.Length > 0 -> "&tbs=cdr%3A1" + something
        | _ -> ""

    result + datePart

let GetGoogleLinks (term, fromDate: Option<DateTime>, toDate: Option<DateTime>)=
    let googleLinks = HtmlDocument.Load(GetGoogleSearchUrl(term, fromDate, toDate)).Descendants ["a"]
    googleLinks
        |> Seq.choose (fun x -> 
                x.TryGetAttribute("href")
                |> Option.map (fun a -> x.InnerText(), a.Value()))
        |> Seq.filter (fun (name, url) -> 
                    name <> "Cached" && name <> "Similar" && url.StartsWith("/url?"))
        |> Seq.map (fun (_, url) -> url.Substring(0, url.IndexOf("&sa=")).Replace("/url?q=", ""))
        |> Seq.filter (fun (url) -> url.StartsWith("http"))
        |> Seq.toArray
