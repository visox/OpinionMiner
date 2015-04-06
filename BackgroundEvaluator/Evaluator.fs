namespace BackgroundEvaluator

open System
open System.Linq
open OpinionMinerData

module Evaluator = 

    let private context = new OpinionMinerDBEntities()

    let private executeForRequest requestId resolvedGuid =
        let request = context.OpinionRequest |> Seq.filter(fun r -> r.Id.Equals(requestId)) |> Seq.exactlyOne
        request.ResolvedBy <- resolvedGuid.ToString()
        let urls = UrlSource.fakeUrls request.Term |> Seq.truncate request.UrlsCount //todo possibly not enough urls
        let mutable results = []
        urls 
            |> Observable.ofSeq
            |> Observable.map(WebParser.LoadPage)
            |> Observable.subscribe(fun p -> 
                    results <- (p.pureText |> EmotionalTextEvaluator.EvaluateText) :: results
                    request.Result <- results 
                                        |> List.average
                                        |> Nullable<float>
                    request.PartlyEvaluated <- (float (request.UrlsCount / (results |> List.length))) |> Nullable<float>
                    context.SaveChanges() |> ignore)       


    let startExecutingRequestAsync =
        lock context (fun () ->
            context.OpinionRequest 
            |> Array.ofSeq
            |> Array.filter(fun r -> r.ResolvedBy |> String.IsNullOrEmpty)
            |> Array.Parallel.iter(fun r -> (executeForRequest r.Id Guid.NewGuid) |> ignore))
        ()