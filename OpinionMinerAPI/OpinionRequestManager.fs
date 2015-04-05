module OpinionRequestManager

open System

open OpinionMinerData
open OpinionMinerAPI.Models

let context = new OpinionMinerDBEntities()

let rec AddPartlyRequests term repeate (toDate: DateTime) pageCount part =
    match part with
    | over when over >= repeate -> ()
    | _ ->
        context.OpinionRequest.Add(
            OpinionMinerData.OpinionRequest(
                        Created = DateTime.Now,
                        Term = term, 
                        ToDate = toDate.AddMonths(-part),
                        FromDate = toDate.AddMonths(-(1+part)),
                        PageCount = pageCount)) |> ignore
        AddPartlyRequests term repeate toDate pageCount (1 + part)
    
//let AddOpinionRequest (request : OpinionRequest) =
//    let savedRequest = context.OpinionRequest.Add(
//                        OpinionMinerData.OpinionRequest(
//                                    Created = request.Created,
//                                    Term = request.Term, 
//                                    ToDate = request.ToDate,
//                                    DayCycle = request.DayCycle,
//                                    Repeate = request.Repeate,
//                                    PageCount = request.PageCount))
//    AddPartlyRequests savedRequest savedRequest.Repeate
//    ()
//    public int Id { get; set; }
//        public System.DateTime Created { get; set; }
//        public string Term { get; set; }
//        public System.DateTime ToDate { get; set; }
//        public int DayCycle { get; set; }
//        public int Repeate { get; set; }
//        public int PageCount { get; set; }