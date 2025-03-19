namespace ProjectManagement.API.RequestModels.File
{
    public class FilesGet
    {
        public Guid Id { get; set; }
        public int ProjectId { get; set; }

        public string Name { get; set; }
    }
}
