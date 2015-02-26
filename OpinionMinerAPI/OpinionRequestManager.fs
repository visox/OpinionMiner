module OpinionRequestManager

open OpinionMinerData
open OpinionMinerAPI.Models

let context = new OpinionMinerDBEntities()

let rec AddPartlyRequests (request : OpinionMinerData.OpinionRequest) part =
    match part with
    | over when over >= request.Repeate -> ()
    | _ ->
        context.Opinion.Add(
            OpinionMinerData.Opinion(
                        OpinionRequestId = request.Id,
                        Created = request.Created,
                        Term = request.Term, 
                        ToDate = request.ToDate.AddDays((float request.DayCycle * float part)),
                        FromDate = request.ToDate.AddDays((float request.DayCycle * float (part + 1))),
                        PageCount = request.PageCount)) |> ignore
        AddPartlyRequests request (1 + part)
    
let AddOpinionRequest (request : OpinionRequest) =
    let savedRequest = context.OpinionRequest.Add(
                        OpinionMinerData.OpinionRequest(
                                    Created = request.Created,
                                    Term = request.Term, 
                                    ToDate = request.ToDate,
                                    DayCycle = request.DayCycle,
                                    Repeate = request.Repeate,
                                    PageCount = request.PageCount))
    AddPartlyRequests savedRequest savedRequest.Repeate
    ()
//    public int Id { get; set; }
//        public System.DateTime Created { get; set; }
//        public string Term { get; set; }
//        public System.DateTime ToDate { get; set; }
//        public int DayCycle { get; set; }
//        public int Repeate { get; set; }
//        public int PageCount { get; set; }