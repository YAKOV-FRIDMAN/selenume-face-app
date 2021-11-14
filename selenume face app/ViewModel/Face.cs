using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selenume_face_app.ViewModel
{
    public class ImgSize
    {
        public int h { get; set; }
        public int w { get; set; }
    }

    public class Emotions
    {
        /// <summary>
        /// עצב
        /// </summary>
        public int sadness { get; set; }
        /// <summary>
        /// גועל
        /// </summary>
        public int disgust { get; set; }
        /// <summary>
        /// כַּעַס
        /// </summary>
        public int anger { get; set; }
        /// <summary>
        /// הַפתָעָה
        /// </summary>
        public int surprise { get; set; }
        /// <summary>
        /// פַּחַד
        /// </summary>
        public int fear { get; set; }
        /// <summary>
        /// אושר
        /// </summary>
        public int happiness { get; set; }
    }

    public class Position
    {
        public int y { get; set; }
        public int x { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Rotation
    {
        public int yaw { get; set; }
        public int roll { get; set; }
        public int pitch { get; set; }
    }

    public class Righteye
    {
        public int y { get; set; }
        public int x { get; set; }
    }

    public class Lefteye
    {
        public int y { get; set; }
        public int x { get; set; }
    }

    public class Maskpoint
    {
        public int y { get; set; }
        public int x { get; set; }
    }

    public class Landmarks
    {
        public Righteye righteye { get; set; }
        public Lefteye lefteye { get; set; }
        public List<Maskpoint> maskpoints { get; set; }
    }

    public class Ethnicity
    {
        public int caucasian { get; set; }
        public int hispanic { get; set; }
        public int asian { get; set; }
        public int african { get; set; }
    }

    public class Person
    {
        public int mood { get; set; }
        public int gender { get; set; }
        public int age { get; set; }
        public List<string> clothingcolors { get; set; }
        public Emotions emotions { get; set; }
        public Position position { get; set; }
        public Rotation rotation { get; set; }
        public Landmarks landmarks { get; set; }
        public Ethnicity ethnicity { get; set; }
    }

    public class Face
    {
        public ImgSize img_size { get; set; }
        public int error_code { get; set; }
        public string description { get; set; }
        public List<Person> people { get; set; }
    }
}
