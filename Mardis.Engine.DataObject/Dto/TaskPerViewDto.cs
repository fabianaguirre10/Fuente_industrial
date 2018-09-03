using System;

namespace Mardis.Engine.DataObject.Dto
{
    public class TaskPerViewDto
    {
        public int Count { get; set; }

        public Guid IdStatusTask { get; set; }
        public string StatusName { get; internal set; }
    }
}
