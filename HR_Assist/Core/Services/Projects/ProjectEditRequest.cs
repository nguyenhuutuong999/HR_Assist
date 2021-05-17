namespace HR_Assist.Core.Services.Projects
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using MediatR;
    using HR_Assist.Core.Services.Common.Models;

    public class ProjectEditRequest : IRequest<ResponseModel>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Technology { get; set; } = string.Empty;

        public string Domain { get; set; } = string.Empty;

        public int Size { get; set; }

    }
}
