using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Data_Transfer_Objects;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        private readonly IMapper _mapper;

        public ReservationController(ParasocialsPOSAPIDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;

        }

        [HttpPost]
        [Route("/reservation")]
        public async Task<IActionResult> AddReservation(DateTime createDate, DateTime reservationDate, DateTime duration, string reservationNotes, PremisesType premiseType, string location)
        {
            var reservation = new Reservation()
            {
                ReservationId = new Guid(),
                CreatedDate = createDate,
                ReservationDate = reservationDate,
                Duration = duration,
                ReservationNotes = reservationNotes
            };

            var premise = new Premise()
            {
                PremiseId = new Guid(),
                Type = premiseType,
                Location = location,
                ReservationId = reservation.ReservationId,
                Reservation = reservation
            };

            reservation.Premise = premise;
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<ReservationDTO>(reservation));
        }

        [HttpGet]
        [Route("/reservation/{reservationId:guid}")]
        public async Task<IActionResult> GetReservationById([FromRoute] Guid reservationId)
        {
            var reservation = await dbContext.Reservations.Include(e => e.Premise).Where(c => c.ReservationId == reservationId).FirstOrDefaultAsync();
            if (reservation != null)
            {
                return Ok(_mapper.Map<ReservationDTO>(reservation));
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/reservation")]
        public async Task<IActionResult> GetReservationList()
        {
            return Ok(_mapper.Map<List<ReservationDTO>>(await dbContext.Reservations.Include(e => e.Premise).ToListAsync()));
        }

        [HttpDelete]
        [Route("/reservationDeleteById/{id:guid}")]
        public async Task<IActionResult> ReservationProductById([FromRoute] Guid id)
        {
            var reservation = await dbContext.Reservations.FindAsync(id);
            if (reservation != null)
            {
                dbContext.Reservations.Remove(reservation);
                await dbContext.SaveChangesAsync();
                return Ok(_mapper.Map<ReservationDTO>(reservation));
            }
            return NotFound();
        }

        [HttpPut]
        [Route("/ReservationUpdateById/{id:guid}")]
        public async Task<IActionResult> UpdateReservationById([FromRoute] Guid id, DateTime createDate, DateTime reservationDate, DateTime duration, string reservationNotes, PremisesType type, string location)
        {
            var reservation = await dbContext.Reservations.Include(e => e.Premise).Where(c => c.ReservationId == id).FirstOrDefaultAsync();
            if (reservation != null)
            {
                reservation.CreatedDate = createDate;
                reservation.ReservationDate = reservationDate;
                reservation.Duration = duration;
                reservation.ReservationNotes = reservationNotes;
                reservation.Premise.Type = type;
                reservation.Premise.Location = location;
                await dbContext.SaveChangesAsync();
                return Ok(_mapper.Map<ReservationDTO>(reservation));
            }
            return NotFound();
        }
    }
}
