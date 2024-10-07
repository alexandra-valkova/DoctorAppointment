using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Application.Features.Appointments.DTOs
{
    public record AppointmentDTO
    {
        public int Id { get; internal set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public AppointmentStatus? Status { get; set; }

        public static implicit operator AppointmentDTO(Appointment appointment)
        {
            return new AppointmentDTO
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Date = appointment.Date,
                Status = appointment.Status
            };
        }

        public static implicit operator Appointment(AppointmentDTO dto)
        {
            return new Appointment
            {
                Id = dto.Id,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                Date = dto.Date,
                Status = dto.Status.GetValueOrDefault()
            };
        }
    }
}