namespace OpinionMinerAPI.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net
open System.Net.Http
open System.Web.Http
open OpinionMinerAPI.Models

/// Retrieves values.
type OpinionRequestController() =
    inherit ApiController()

    member x.Put() =
        x.Request.CreateResponse(HttpStatusCode.OK)
