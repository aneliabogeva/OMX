﻿namespace OMX.Models
{
    public class PropertyFeature
    {
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
