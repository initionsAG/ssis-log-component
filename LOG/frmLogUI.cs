using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using Microsoft.SqlServer.Dts.Runtime;
using System.Collections.Generic;

namespace Isag.Log
{
    public partial class frmLogUI : Form
    {
        private TaskHost _taskHost;
        //returns the assmelby / file version of the component
        private string Version
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
                return fvi.FileVersion;
            }
        }

        public frmLogUI(TaskHost taskHost)
        {
            _taskHost = taskHost;
            
            InitializeComponent();
            this.Text += " " + Version;

            PopulateComponentTypeComboBox();
            PopulateVariablesComboBox();
            InitValues();
        }

        private void InitValues()
        {
            try{
                tbMessage.Text = _taskHost.Properties["MessageText"].GetValue(_taskHost).ToString();
            }catch{
                tbMessage.Text = "";
            }

            try{
                cmbComponentType.SelectedValue = _taskHost.Properties["ComponentType"].GetValue(_taskHost);
            }catch{
                cmbComponentType.SelectedValue = ComponentType.GetDefaultComponentId();
            }
            //Init cmbVariables set to nothing
            cmbVariables.ResetText();
            cmbVariables.SelectedIndex = -1;
        }

        /// <summary>
        /// Sets the Source for the ComponentType Combobox
        /// </summary>
        private void PopulateComponentTypeComboBox()
        {
            Dictionary<string,object > componentTypeList = new Dictionary<string, object>();
            foreach (var componentType in ComponentType.ComponentTypes)
            {
                componentTypeList.Add(componentType.Key + "[" + componentType.Value.ToString() + "]", componentType.Value);
            }            
            cmbComponentType.DataSource = new BindingSource(componentTypeList, null);
            cmbComponentType.DisplayMember = "Key";
            cmbComponentType.ValueMember = "Value";
        }

        /// <summary>
        /// Sets the Source / Values for the Variables Combobox
        /// </summary>
        private void PopulateVariablesComboBox()
        {
            List<string> variableList = new List<string>();
            foreach (Variable variable in _taskHost.Variables)
            {
                variableList.Add(variable.QualifiedName);
            }
            variableList.Sort();
            cmbVariables.DataSource = new BindingSource(variableList, null);
        }

        /// <summary>
        /// Event handler for button click
        /// sets the custom ssis properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //Saving Values
            _taskHost.Properties["MessageText"].SetValue(_taskHost, tbMessage.Text);
            _taskHost.Properties["ComponentType"].SetValue(_taskHost, cmbComponentType.SelectedValue);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (cmbVariables.SelectedValue != null)
            {
                System.Windows.Forms.Clipboard.SetDataObject("@(" + cmbVariables.SelectedValue.ToString() + ")", true);
                tbMessage.Paste();
            }
        }
    }
}
