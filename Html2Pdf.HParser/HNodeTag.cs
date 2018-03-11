using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public abstract class HNodeTag: HNode
    {
        private HTagType tagType;
        public HTagType TagType { get => tagType; }


        private IEnumerable<HAttribute> attributes;
        private IEnumerable<HStyle> styles;

        public List<HAttribute> Attributes { get => attributes as List<HAttribute>; private set => attributes = value; }
        public List<HStyle> Styles { get => styles as List<HStyle>; private set => styles = value; }


        public HNodeTag(HTagType tagType, IEnumerable<HAttribute> attributes, IEnumerable<HStyle> styles)
        {
            this.tagType = tagType;
            this.attributes = attributes;
            this.styles = styles;
        }

    }
}
