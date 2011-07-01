<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamCitySuccess.aspx.cs" Inherits="TestPages.TeamCitySuccess" %>

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
            <img src="http://win7ci:8080/img/buildStates/success.gif" style="" title='Build configuration is successful' alt='Build configuration is successful' />
            <a class="buildTypeName" style=""
     href="http://win7ci:8080/viewType.html?buildTypeId=bt2&tab=buildTypeStatusDiv" 
     title="Click to open &quot;BuildConfig&quot; build configuration home page">BuildConfig</a>
          </td>

          
          <td class="buildNumberDate">
            
              <div class="teamCityBuildNumber">build <a href="http://win7ci:8080/viewLog.html?buildId=9&tab=buildResultsDiv&buildTypeId=bt2" title="View build results" >#9</a>
</div>
              <div class="teamCityDateTime"><span class="date" title="12 hours ago">02&nbsp;May&nbsp;11&nbsp;00:02</span></div>
            
          </td>
          <td class="buildResults">
            
              <div class="teamCityBuildResults">
                
                
                  <img src="http://win7ci:8080/img/buildStates/success_small.gif" id="" class="icon"  style=""
  title='' alt=''
/><div id="dataHover_9" style="display: none;">Build was successful (build agent Win7CI)</div>
                  <a href="http://win7ci:8080/viewLog.html?buildId=9&tab=buildResultsDiv&buildTypeId=bt2" title="View build results" >results</a>
                
              </div>
            
          </td>
        </tr>

      
    </table>


