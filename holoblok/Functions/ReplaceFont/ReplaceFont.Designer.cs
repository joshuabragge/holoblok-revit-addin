//
// Created by SharpDevelop.
// User: michael
// Date: 7/26/2014
// Time: 10:39 PM
// 
// To change this template use Tools | Options | Coding | Edit Standard Headers.
//
// ERROR: Not supported in C#: OptionDeclaration
namespace font
{

	///
	[System.CLSCompliantAttribute(false)]
	public sealed partial class ReplaceFont : Autodesk.Revit.UI.Macros.DocumentEntryPoint
	{

		public event System.EventHandler Startup;

		public event System.EventHandler Shutdown;

		///
		[System.Diagnostics.DebuggerNonUserCodeAttribute(), System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
		private void OnStartup()
		{
			Globals.ReplaceFont = this;
		}

		///
		[System.Diagnostics.DebuggerNonUserCodeAttribute(), System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override void FinishInitialization()
		{
			base.FinishInitialization();
			this.OnStartup();
			if ((((this.Startup) != null))) {
				if (Startup != null) {
					Startup(this, System.EventArgs.Empty);
				}
			}
		}

		///
		[System.Diagnostics.DebuggerNonUserCodeAttribute(), System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override void OnShutdown()
		{
			if ((((this.Shutdown) != null))) {
				if (Shutdown != null) {
					Shutdown(this, System.EventArgs.Empty);
				}
			}
			base.OnShutdown();
		}

		[System.Diagnostics.DebuggerNonUserCodeAttribute(), System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override string PrimaryCookie {
			get { return "ReplaceFont"; }
		}
	}
}
namespace font
{

	///
	public sealed partial class Globals
	{

		private static ReplaceFont _ReplaceFont;

		static internal ReplaceFont ReplaceFont {
			get { return _ReplaceFont; }
			set {
				if ((_ReplaceFont == null)) {
					_ReplaceFont = value;
				} else {
					throw new System.NotSupportedException();
				}
			}
		}
	}
}
