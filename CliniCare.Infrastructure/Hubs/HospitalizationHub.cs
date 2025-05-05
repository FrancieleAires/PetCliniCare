using CliniCare.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Infrastructure.Hubs
{
    public class HospitalizationHub : Hub
    {
        public async Task SendHospitalizationUpdate(int animalId, string updateMessage)
        {
            await Clients.All.SendAsync("ReceiveHospitalizationUpdate", animalId, updateMessage);
        }
    }

}
