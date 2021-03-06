using System;
using System.Text;
using System.Collections.Generic;
using Symitar;

namespace PwrIDE
{
  public class Importer
  {
  	//========================================================================
  	private Project impPrj = null;
  	private SymInst impSym = null;
  	private Local   impLoc = null;
  	private string  impPth = null;
  	//========================================================================
  	public Importer(Project owner)
  	{
  		impPrj = owner;
  		if(impPrj.Local)
  			impLoc = impPrj.ParentLocal;
  		else
  			impSym = impPrj.ParentSym;
  	}
  	//------------------------------------------------------------------------
  	public Importer(SymInst owner)
  	{
  		impSym = owner;
  	}
  	//------------------------------------------------------------------------
  	public Importer(Local owner)
  	{
  		impLoc = owner;
  	}
  	//------------------------------------------------------------------------
  	public Importer(string path)
  	{
  		impPth = path;
  	}
  	//------------------------------------------------------------------------
  	public Importer()
  	{
  	}
  	//========================================================================
  	public Importer CopyOf()
  	{
  		if(impPrj != null) return new Importer(impPrj);
  		if(impSym != null) return new Importer(impSym);
  		if(impLoc != null) return new Importer(impLoc);
  		if(impPth != null) return new Importer(impPth);
  		                   return new Importer();
  	}
  	//========================================================================
  	public string Import(string filename)
  	{
  		try
  		{
	  		if(impPrj != null)
	  		{
	  			ProjectFile file = impPrj.GetFile(filename);
	  			if(file != null)
            return file.Read();
	  		}
	  		if(impSym != null)
	  			if(Util.TrySymConnect(impSym))
	  				if(Util.FileExistsSym(impSym, filename, SymFile.Type.REPGEN))
	  					return Util.FileReadSym(impSym, filename, SymFile.Type.REPGEN);
	  		if(impLoc != null)
	  			if(Util.FileExistsLocal(impLoc.Path+'\\'+filename))
	  				return Util.FileReadLocal(impLoc.Path+'\\'+filename);
	  		if(impPth != null)
	  			if(Util.FileExistsLocal(impPth+'\\'+filename))
	  				return Util.FileReadLocal(impPth+'\\'+filename);
  		}
  		catch(Exception) { }
  		return null;
  	}
  	//========================================================================
  }
}
