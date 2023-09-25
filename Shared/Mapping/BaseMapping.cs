using Domain.Dto._Base;
using Newtonsoft.Json;
using Shared.Model._Base;

namespace Shared.Mapping
{
    public static class BaseMapping
    {
        public static string ObjectToJson(this object model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public static UploadFileDto ToDto(this UploadFileViewModel model)
        {
            return new UploadFileDto
            {
                FileContent = model.FileContent,
                FileName = model.FileName
            };
        }

    }
}
