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
        public IEnumerable<IncidentManagement_Data> get()
        {
            using(TicketAutomationEntities entities = new TicketAutomationEntities())
            {
                return entities.IncidentManagement_Data.ToList();
            }
        }

        public IncidentManagement_Data get(int id)
        {
            using (TicketAutomationEntities entities = new TicketAutomationEntities())
            {
                return entities.IncidentManagement_Data.FirstOrDefault(e => e.PayrollDataId == id);
            }
        }

        [HttpPatch]
        public string Patch(int id, [FromBody]IncidentManagement_Data ticket)
        {
            using (TicketAutomationEntities entities = new TicketAutomationEntities())
            {
                var entity = entities.IncidentManagement_Data.FirstOrDefault(e => e.PayrollDataId == id);
                entity.Priority = ticket.Priority;
                entity.ETR = ticket.ETR;
                entity.StatusTracking = ticket.StatusTracking;
                entities.SaveChanges();
                return "{success:'ticket updated successfully'}";
            }
        }
    }
}
