using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TicketAutomationAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TicketsController : ApiController
    {
        public IEnumerable<IncidentManagement_WeeklyData> get()
        {
            using(TicketAutomationEntities entities = new TicketAutomationEntities())
            {
                return entities.IncidentManagement_WeeklyData.ToList();
            }
        }

        public IncidentManagement_WeeklyData get(int id)
        {
            using (TicketAutomationEntities entities = new TicketAutomationEntities())
            {
                return entities.IncidentManagement_WeeklyData.FirstOrDefault(e => e.PayrollDataId == id);
            }
        }

        [HttpPatch]
        public string Patch(int id, [FromBody]IncidentManagement_WeeklyData ticket)
        {
            using (TicketAutomationEntities entities = new TicketAutomationEntities())
            {
                var entity = entities.IncidentManagement_WeeklyData.FirstOrDefault(e => e.PayrollDataId == id);
                entity.Priority = ticket.Priority;
                entity.ETR = ticket.ETR;
                entity.StatusTracking = ticket.StatusTracking;
                entities.SaveChanges();
                return "{success:'ticket updated successfully'}";
            }
        }

        [Route("api/tickets/finalize")]
        [HttpPatch]
        public string finalize([FromBody]IEnumerable<IncidentManagement_WeeklyData> tickets)
        {
            foreach(IncidentManagement_WeeklyData ticket in tickets)
            {
                using (TicketAutomationEntities entities = new TicketAutomationEntities())
                {
                    var entity = entities.IncidentManagement_WeeklyData.FirstOrDefault(e =>e.PayrollDataId == ticket.PayrollDataId);
                    entity.Finalized = true;
                    entities.SaveChanges();
                }
            }
            return "{success:'ticket updated successfully'}";
        }
    }
}
