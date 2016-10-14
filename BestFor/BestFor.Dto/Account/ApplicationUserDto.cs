using System;

namespace BestFor.Dto.Account
{
    public class ApplicationUserDto : BaseDto
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// Return user name if blank.
        /// </summary>
        private string _displayName;
        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(_displayName) || string.IsNullOrWhiteSpace(_displayName)) return UserName;
                return _displayName;

            }
            set
            {
                _displayName = value;
            }
        }

        public int NumberOfAnswers { get; set; }

        public int NumberOfDescriptions { get; set; }

        public int NumberOfVotes { get; set; }

        public int NumberOfFlags { get; set; }

        public int NumberOfComments { get; set; }

        /// <summary>
        /// User's favorite opinions category
        /// </summary>
        public string FavoriteCategory { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime DateCancelled { get; set; }

        public bool IsCancelled { get; set; }

        public string CancellationReason { get; set; }
    }
}
