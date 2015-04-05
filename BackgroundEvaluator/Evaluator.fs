namespace BackgroundEvaluator

open System

open OpinionMinerData

module Evaluator = 

    let private context = new OpinionMinerDBEntities()

    let private executeForRequest requestId resolvedGuid =
        let request = context.OpinionRequest |> Seq.filter(fun r -> r.Id.Equals(requestId)) |> Seq.exactlyOne
        let urls = UrlSource.getBingResult request.Term |> Seq.truncate request.UrlsCount //todo possibly not enough urls
        let result = urls 
                    |> Seq.map(fun u -> u |> WebParser.LoadPage)
                    |> Seq.map(fun p -> p.pureText |> EmotionalTextEvaluator.EvaluateText)
                    |> Seq.average
        request.Result <- Nullable<float>(result)
        request.ResolvedBy <- resolvedGuid.ToString()
        request.PartlyEvaluated <- Nullable<float>(1.)
        context.SaveChanges |> ignore
        ()


    let startExecutingRequestAsync =
        lock context (fun () ->
            context.OpinionRequest 
            |> Array.ofSeq
            |> Array.filter(fun r -> r.ResolvedBy |> String.IsNullOrEmpty)
            |> Array.Parallel.iter(fun r -> executeForRequest r.Id Guid.NewGuid))
        ()