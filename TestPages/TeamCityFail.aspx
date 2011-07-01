<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamCityFail.aspx.cs" Inherits="TestPages.TeamCityFail" %>





  

    <table class="tcTable">
      <tr>
        <td colspan="3" class="tcTD_projectName">
          <div class="projectName">
            

<a class="buildTypeName" style=""
     href="http://win7ci:8080/project.html?projectId=project2&tab=projectOverview"
     
     title="Click to open &quot;CiTest&quot; project home page"
     >CiTest</a>
          </div>
        </td>
      </tr>

      
        

        <tr>
          <td class="buildConfigurationName">
            <img src="http://win7ci:8080/img/buildStates/error.gif" style="" title='Build configuration is failing' alt='Build configuration is failing' />
            <a class="buildTypeName" style=""
     href="http://win7ci:8080/viewType.html?buildTypeId=bt2&tab=buildTypeStatusDiv" 
     title="Click to open &quot;BuildConfig&quot; build configuration home page">BuildConfig</a>
          </td>

          
          <td class="buildNumberDate">
            
              <div class="teamCityBuildNumber">build <a href="http://win7ci:8080/viewLog.html?buildId=14&tab=buildResultsDiv&buildTypeId=bt2" title="View build results" >#14</a>
</div>
              <div class="teamCityDateTime"><span class="date" title="moments ago">02&nbsp;May&nbsp;11&nbsp;23:00</span></div>
            
          </td>
          <td class="buildResults">
            
              <div class="teamCityBuildResults">
                
                
                  <img src="http://win7ci:8080/img/buildStates/error_small.gif" id="" class="icon"  style=""
  title='' alt=''
/><div id="dataHover_14" style="display: none;">Build failed (build agent Win7CI)</div>
                  <a href="http://win7ci:8080/viewLog.html?buildId=14&tab=buildResultsDiv&buildTypeId=bt2" title="View build results" >results</a>
                
              </div>
            
          </td>
        </tr>

      
    </table>


