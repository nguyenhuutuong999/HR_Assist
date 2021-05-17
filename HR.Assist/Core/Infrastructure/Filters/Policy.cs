namespace HR.Assist.Core.Infrastructure.Filters
{
    public class Policy
    {
        public static readonly string[] Policies =
        {
           
            CanViewUser, CanCreateUser, CanEditUser, CanDeleteUser,
            CanViewProject, CanCreateProject, CanEditProject, CanDeleteProject,
            CanViewTeam, CanCreateTeam, CanEditTeam, CanDeleteTeam,
            CanViewMasterData
        };

       

        public const string CanViewProject = "CanViewProject_*_*";
        public const string CanCreateProject = "CanCreateProject_*_*";
        public const string CanEditProject = "CanEditProject_*_*";
        public const string CanDeleteProject = "CanDeleteProject_*_*";

        public const string CanViewTeam = "CanViewTeam_*_*";
        public const string CanCreateTeam = "CanCreateTeam_*_*";
        public const string CanEditTeam = "CanEditTeam_*_*";
        public const string CanDeleteTeam = "CanDeleteTeam_*_*";

        public const string CanViewUser = "CanViewUser_*_*";
        public const string CanCreateUser = "CanCreateUser_*_*";
        public const string CanEditUser = "CanEditUser_*_*";
        public const string CanDeleteUser = "CanDeleteUser_*_*";

        public const string CanViewMasterData = "CanViewMasterData_*_*";
    }
}
