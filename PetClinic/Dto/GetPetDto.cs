namespace PetClinic.Dto
{
    public class GetPetDto
    {
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string PetBreed { get; set; }
        public string MedicalHistory { get; set; }
        public List<GetPetsAppointmentsDto> Appointments { get; set; }
    }

    public class GetPetsAppointmentsDto
    {
        public string AppointmentStartTime { get; set; }
        public string AppointmentEndTime { get; set; }
        public string AppointmentClinicName { get; set; }
        public string AppointmentVetName { get; set; }
    }
}

