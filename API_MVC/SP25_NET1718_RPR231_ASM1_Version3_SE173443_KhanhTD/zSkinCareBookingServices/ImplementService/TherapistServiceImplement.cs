using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories;
using zSkinCareBookingRepositories_.DTO;
using zSkinCareBookingRepositories_.Models;
using zSkinCareBookingServices_.InterfaceService;

namespace zSkinCareBookingServices_.ImplementService
{
	public class TherapistServiceImplement : TherapistServiceInterface
	{
		private readonly TherapistRepository _therapistRepository;

		public TherapistServiceImplement(TherapistRepository therapistRepository)
		{
			_therapistRepository = therapistRepository;
		}
		public async Task<int> CreateTherapist(TherapistDTO therapistDTO)
		{
			Therapist therapist = new Therapist();
			therapist.UserId = therapistDTO.UserId;
			therapist.Fullname = therapistDTO.Fullname;
			therapist.Email = therapistDTO.Email;
			therapist.ExpMonth = therapistDTO.ExpMonth;
			therapist.Bio = therapistDTO.Bio;
			therapist.Phone = therapistDTO.Phone;
			therapist.Specialization = therapistDTO.Specialization;
			therapist.CreateAtDateTime = DateTime.Now;
			return await _therapistRepository.CreateAsync(therapist);
		}

		public async Task<bool> DeleteTheraPistById(int therapistId)
		{
			return await _therapistRepository.DeleteTherapistById(therapistId);
		}

		public async Task<Therapist> GetTherapistById(int therapistId)
		{
			return await _therapistRepository.GetByIdAsync(therapistId);
		}

		public async Task<List<Therapist>> GetTherapists()
		{
			return await _therapistRepository.GetAllTherapist();
		}

		public async Task<List<Therapist>> Search(string fullName, string phone, string email, string specialization, int exp, string bio)
		{
			return await _therapistRepository.SearchTherapist(fullName, phone, email, specialization, exp, bio);
		}

		public async Task<int> UpdateTherapist(TherapistDTO therapistDTO)
		{

			Therapist therapist = _therapistRepository.GetById(therapistDTO.Id);
			//therapist.UserId = therapistDTO.UserId;
			therapist.Fullname = therapistDTO.Fullname;
			therapist.Email = therapistDTO.Email;
			therapist.Specialization = therapistDTO.Specialization;
			therapist.Bio = therapistDTO.Bio;
			therapist.Phone = therapistDTO.Phone;
			therapist.ExpMonth = therapistDTO.ExpMonth;
			therapist.UpdateAtDateTime = DateTime.Now;
			return await _therapistRepository.UpdateAsync(therapist);
		}
	}
}
