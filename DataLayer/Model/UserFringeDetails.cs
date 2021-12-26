using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Model
{
    public class UserFringeDetails
    {
        public long Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Gender Gender { get; set; }
        public long? GenderId { get; set; }
        public bool IsTextMessageContact { get; set; }
        public AncestryIdentity AncestryIdentity { get; set; }
        public int? AncestryIdentityId { get; set; }
        public ResponseType SalivaBloodSharing { get; set; }
        public int? SalivaBloodSharingId { get; set; }
        public ResponseType ClinicalTrials { get; set; }
        public int? ClinicalTrialsId { get; set; }
        public Nationality Nationality { get; set; }
        public int? NationalityId { get; set; }
        public PlatformDiscoveryType PlatformDiscoveryType { get; set; }
        public int? PlatformDiscoveryTypeId { get; set; }
        public string ReferalPersonName { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public SexualOrientation SexualOrientation { get; set; }
        public int? SexualOrientationId { get; set; }
        public ResponseType MemberBlackCommunity { get; set; }
        public int? MemberBlackCommunityId { get; set; }
        public ResponseType ArmedForceVeteran { get; set; }
        public int? ArmedForceVeteranId { get; set; }
        public Region Region { get; set; }
        public long? RegionId { get; set; }








    }
}
