namespace EventEase_Booking_System.AzureBlobStorage
{
    public interface IBlobService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);

        Task<bool> DeleteFileAsync(string fileName);
    }

}
