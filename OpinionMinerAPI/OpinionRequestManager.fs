module OpinionRequestManager

open System

open OpinionMinerData
open OpinionMinerAPI.Models

let context = new OpinionMinerDBEntities()

let rec AddPartlyRequests term repeate (toDate: DateTime) pageCount part =
    match part with
    | over when over >= repeate -> ()
    | _ ->
        lock context (fun ()->
            match context.OpinionRequest 
                |> Seq.exists(
                                fun(r) -> 
                                r.Term.Equals <| term &&
                                r.ToDate.Equals <| toDate.AddMonths(-part) &&
                                r.ToDate.Equals <| toDate.AddMonths(-(1+part)) &&
                                r.PageCount.Equals <| pageCount) with
            | true -> ()
            | false ->
                context.OpinionRequest.Add(
                    OpinionMinerData.OpinionRequest(
                        Created = DateTime.Now,
                        Term = term, 
                        ToDate = toDate.AddMonths(-part),
                        FromDate = toDate.AddMonths(-(1+part)),
                        PageCount = pageCount)) |> ignore
                context.SaveChanges |> ignore)
        AddPartlyRequests term repeate toDate pageCount (1 + part)