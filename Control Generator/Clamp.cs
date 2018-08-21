using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Control_Generator
{
    public class Clamp
    {
        public string Clamptype { get; set; }
        public string Serialnum { get; set; }
        public string Rado { get; set; }
        public string Rn { get; set; }
        public string Controldate { get; set; }
        public string Barcode { get; set; }
        public string Rfcable { get; set; }
        public string Rfcablerror { get; set; }
        public string Solenoidcable { get; set; }
        public string Solenoidcablerror { get; set; }
        public string Smb { get; set; }
        public string Smberror { get; set; }
        public string Armature { get; set; }
        public string Armaturerror { get; set; }
        public string Edges { get; set; }
        public string Edgeserror { get; set; }
        public string Paralelity { get; set; }
        public string Paralelityerror { get; set; }
        public string Distance { get; set; }
        public string Distancerror { get; set; }
        public string Rflenght { get; set; }
        public string Rflenghterror { get; set; }
        public string Solenoidlenght { get; set; }
        public string Solenoidlenghterror { get; set; }
        public string Electrodes { get; set; }
        public string Electrodeserror { get; set; }
        public string Bigresistance { get; set; }
        public string Bigresistancerror { get; set; }
        public string Smallresistance { get; set; }
        public string Smallresistancerror { get; set; }
        public string Shortcircuit { get; set; }
        public string Shortcircuiterror { get; set; }
        public string Emptythick { get; set; } 
        public string Emptythickerror { get; set; }
        public string Fullthick { get; set; }
        public string Fullthickerror { get; set; }
        public string Emptythin { get; set; }
        public string Emptythinerror { get; set; }
        public string Fullthin { get; set; }
        public string Fullthinerror { get; set; }
        
        public string Clampimputerror { get; set; } //ovo sutra iskoristi da napravis gde je nastao error...nekako

        public Clamp() { }
    }
}
