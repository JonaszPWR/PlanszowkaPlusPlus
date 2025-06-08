using PlanszowkaPlusPlus.Data;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Services;
using System.Globalization;

namespace PlanszowkaPlusPlus.Services
{
    public class ReservationReminderService
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public ReservationReminderService(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<int> SendRemindersAsync()
        {
            var tomorrow = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

            var reservations = await _context.Reservations
                .Include(r => r.Member)
                .Include(r => r.GameTable)
                .Where(r => r.ReservationDate == tomorrow && !r.IsArchived)
                .ToListAsync();

            foreach (var reservation in reservations)
            {
                var message = $"""
                Hello {reservation.Member.Name},

                This is a reminder that you have a game reservation tomorrow:

                🗓 Date: {reservation.ReservationDate}
                ⏰ Time: {reservation.TimeStart:hh\\:mm} – {reservation.TimeEnd:hh\\:mm}
                🎲 Table: {reservation.TableId}

                If you cannot attend, please cancel your reservation.

                Best regards,  
                Board Game Club
                """;

                await _emailService.SendEmailAsync(
                    reservation.Member.Email,
                    "Reminder: Your reservation is tomorrow",
                    message
                );
            }

            return reservations.Count;
        }
    }
}
