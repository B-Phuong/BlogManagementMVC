using BlogManagement_MVC.Data;

namespace BlogManagement_MVC.Data
{
    public class PostTag
    {
        public long TagId { get; set; }
        public Tag Tag { get; set; }    
        public long PostId { get; set; }
        public Post Post { get; set; }

    }
}
