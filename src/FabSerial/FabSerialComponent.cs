using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;


// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace FabSerial
{
    public class FabSerialComponent : GH_Component
    {
        static SerialPort _serialPort;
        private List<int> pointCloud = null;
        private bool portStatus = false;
        private bool portIsOpen = false;
        private string keyVal = "";

        public int val = 0;
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public FabSerialComponent()
          : base("FabSerial", "FS",
              "Serial port data manipulation",
              "Fab", "Arduino")
        {
        }

        public void CallExpireSolution()
        {
            this.ExpireSolution(true);
        }


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Open", "O", "Open communication", GH_ParamAccess.item);
            pManager.AddTextParameter("Port name", "Name", "COM5, for example", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Text Values", "T", "Text values list", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Point Cloud", "PC", "Points list", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Val", "V", "Value", GH_ParamAccess.item);
        }

        private void ScheduleDelegate(GH_Document doc)
        {
            ExpireSolution(false);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string portName = null;
            string x = null;

            DA.GetData(0, ref portStatus);
            DA.GetData(1, ref portName);

            if (!portIsOpen && portStatus)
            {
                _serialPort = new SerialPort();
                _serialPort.PortName = portName;
                _serialPort.BaudRate = 9600;
                _serialPort.DtrEnable = true;
                _serialPort.Open();

                pointCloud = new List<int>();
                portIsOpen = true;

                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Port  is now OPEN!");
            }
            else
            {
                x = _serialPort.ReadExisting();
                string l = _serialPort.ReadLine();
                try
                {
                    keyVal = l;
                    string[] dict = x.Split(':');
                    val = int.Parse(dict[1]);
                    pointCloud.Add(val);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    Console.WriteLine("Yo");
                    if (portIsOpen && !portStatus)
                    {
                        _serialPort.Close();
                        portIsOpen = false;
                    }
                }
            }
            DA.SetData(0, keyVal);
            DA.SetDataList(1, pointCloud);
            DA.SetData(2, val);
            
            base.OnPingDocument().ScheduleSolution(20, new GH_Document.GH_ScheduleDelegate(ScheduleDelegate));
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return Properties.Resources.serialIcon;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("09e9b942-f529-47c4-a106-bf2a0266ebe3"); }
        }
    }
}
