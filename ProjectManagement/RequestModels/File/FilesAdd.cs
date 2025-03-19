namespace ProjectManagement.API.RequestModels.File
{
    public class FilesAdd
    {
        public Guid Id { get; set; }
        public int projectId { get; set; }

        public string Name { get; set; }
    }
}
