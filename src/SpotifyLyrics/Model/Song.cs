namespace SpotifyLyrics.Model
{
    public class Song
    {
        protected bool Equals(Song other)
        {
            return string.Equals(Artist, other.Artist) && string.Equals(Title, other.Title);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Artist != null ? Artist.GetHashCode() : 0)*397) ^ (Title != null ? Title.GetHashCode() : 0);
            }
        }

        public string Artist { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return Artist + " - " + Title;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Song) obj);
        }
    }
}