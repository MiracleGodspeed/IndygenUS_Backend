using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Dtos
{
   public class UpdateProfileDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public bool IsTextMessageContact { get; set; }
        public long? GenderId { get; set; }
        public int? AncestryId { get; set; }
        public int? NationalityId { get; set; }
        public long? RegionId { get; set; }
        public int? PlatformDiscoveryId { get; set; }
        public string ReferalName { get; set; }
        public int? SalivaBloodResponse { get; set; }
        public int? ClinicalTrialsResponse { get; set; }
        public int? MemberBlackCommunityResponse { get; set; }
        public int? ArmedForceVeteranResponse { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public int? SexualOrientationId { get; set; }

    }
}
