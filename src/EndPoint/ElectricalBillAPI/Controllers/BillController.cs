using Entity.Entities;
using Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IElectricBillRepository _ElectricBillRepository;

        public BillController(IElectricBillRepository electricBillRepository)
        {
            _ElectricBillRepository = electricBillRepository;
        }

        [HttpPost("InsertBillForDuration")]
        public async Task InsertBillForDuration(double numberOfPastDays)
        {
            var path = "D:\\duration.txt";
            for (int i = 0; i < numberOfPastDays; i++)
            {
                var start = DateTime.Now;
                await GetBillsAndInsert(DateTime.Now);
                var end = DateTime.Now;
                var duration = (end - start).TotalMilliseconds;
                System.IO.File.AppendAllText(path, $"duration of Ireration {i} : {duration} milliseconds");
                System.IO.File.AppendAllText(path, Environment.NewLine);
            }
        }

        private async Task GetBillsAndInsert(DateTime dateOf)
        {
            var bills = await GetBillForDay(dateOf);
            foreach (var bill in bills)
            {
                _ElectricBillRepository.Insert(bill);
                await _ElectricBillRepository.SaveContextAsync();
            }
        }

        private async Task<List<ElectricBill>> GetBillForDay(DateTime dateOf)
        {
            var list = new List<ElectricBill>();
            for (int i = 0; i < 500; i++)
            {
                var bill = new ElectricBill()
                {
                    Name = "Name",
                    BillId = "BillId",
                    PayId = "PayId",
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now.AddDays(30),
                    ConsumedEnergy = 1000
                };
                list.Add(bill);
            }
            return list;
        }
    }
}
