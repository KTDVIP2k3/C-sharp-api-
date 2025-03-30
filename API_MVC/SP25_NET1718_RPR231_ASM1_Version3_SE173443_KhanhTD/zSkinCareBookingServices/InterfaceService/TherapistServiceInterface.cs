using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories_.DTO;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingServices_.InterfaceService
{
	public interface TherapistServiceInterface
	{
		Task<List<Therapist>> GetTherapists();

		Task<List<Therapist>> Search(String fullName, String phone, String email, String specialization, int exp, String bio);
		Task<int> CreateTherapist(TherapistDTO therapistDTO);

		Task<Therapist> GetTherapistById(int therapistId);

		Task<int> UpdateTherapist(TherapistDTO therapistDTO);

		Task<bool> DeleteTheraPistById(int therapistId);
	}
}
