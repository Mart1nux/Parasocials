using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class ReservationsController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;

        public ReservationsController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("/reservations")]
        public async Task<IActionResult> GetReservationsList()
        {
            return Ok(await dbContext.Reservations.ToListAsync());
        }

        [HttpGet]
        [Route("/reservation/{reservationId:guid}")]
        public async Task<IActionResult> GetReservation([FromRoute] Guid reservationId)
        {
            var reservation = await dbContext.Tips.FindAsync(reservationId);
            System.Console.WriteLine(reservationId);
            if (reservation != null)
            {
                return Ok(reservation);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("/reservation")]
        public async Task<IActionResult> CreateReservation(DateTime createdDate, DateTime reservationDate, DateTime duration, string reservationNotes, string location, PremisesType premisetype)
        {
            var reservation = new Reservation()
            {
                ReservationId = Guid.NewGuid(),
                CreatedDate = createdDate,
                ReservationDate = reservationDate,
                Duration = duration,
                ReservationNotes = reservationNotes,
                Premise = new Premise()
                {
                    PremiseId = Guid.NewGuid(),
                    Type = premisetype,
                    Location = location
                }
            };
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();
            return Ok(reservation);
        }

        [HttpPut]
        [Route("/ReservationUpdateById/{reservationId:guid}")]
        public async Task<IActionResult> UpdateProductById([FromRoute] Guid reservationId, DateTime createdDate, DateTime reservationDate, DateTime duration, string reservationNotes, string location, PremisesType premisetype)
        {
            var reservation = await dbContext.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                reservation.CreatedDate = createdDate;
                reservation.ReservationDate = reservationDate;
                reservation.Duration = duration;
                reservation.ReservationNotes = reservationNotes;
                reservation.Premise.Location = location;
                reservation.Premise.Type = premisetype;
                await dbContext.SaveChangesAsync();
                return Ok(reservation);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("/ReservationsDeleteById/{reservationId:guid}")]
        public async Task<IActionResult> DeleteProductById([FromRoute] Guid reservationId)
        {
            var reservation = await dbContext.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                dbContext.Reservations.Remove(reservation);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

    }
}

