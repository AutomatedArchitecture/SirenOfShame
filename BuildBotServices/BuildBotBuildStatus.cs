using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;
using System.Dynamic;
using System.Collections;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using System.Text;

namespace BuildBotServices
{
    /*
     * {
     *  -1: 
     *  {
     *      builderName: "runtests",
     *      logs: 
     *      [
     *          [
     *              "stdio",
     *              "http://localhost:8010/builders/runtests/builds/3/steps/git/logs/stdio"
     *          ],
     *          [
     *              "stdio",
     *              "http://localhost:8010/builders/runtests/builds/3/steps/shell/logs/stdio"
     *          ]
     *      ],
     *      number: 3,
     *      properties: 
     *      [
     *          [
     *              "blamelist",
     *              [
     *                  "chrome-release@google.com"
     *              ],
     *              "Build"
     *          ],
     *          [
     *              "branch",
     *              "",
     *              "Build"
     *          ],
     *          [
     *              "buildername",
     *              "runtests",
     *              "Builder"
     *          ],
     *          [
     *              "buildnumber",
     *              3,
     *              "Build"
     *          ],
     *          [
     *              "got_revision",
     *              "af28cdb6caf82d9e29fc784823a531a126a374dc",
     *              "Source"
     *          ],
     *          [
     *              "owner",
     *              "pyflakes <pyflakes@localhost>",
     *              "Force Build Form"
     *          ],
     *          [
     *              "owners",
     *              [
     *                  "pyflakes <pyflakes@localhost>"
     *              ],
     *              "The web-page 'force build' button was pressed by 'pyflakes <pyflakes@localhost>': force build"
     *          ],
     *          [
     *              "project",
     *              "",
     *              "Build"
     *          ],
     *          [
                    "reason",
     *              "force build",
     *              "Force Build Form"
     *          ],
     *          [
     *              "repository",
     *              "",
     *              "Build"
     *          ],
     *          [
     *              "revision",
     *              "",
     *              "Build"
     *          ],
     *          [
     *              "scheduler",
     *              "force",
     *              "Scheduler"
     *          ],
     *          [
     *              "slavename",
     *              "example-slave",
     *              "BuildSlave"
     *          ],
     *          [
     *              "workdir",
     *              "E:\My Documents\Code\buildslave\slave/runtests",
     *              "slave"
     *          ]
     *      ],
     *      reason: "The web-page 'force build' button was pressed by 'pyflakes <pyflakes@localhost>': force build",
     *      slave: "example-slave",
     *      steps: 
     *      [
     *          {
     *              expectations: 
     *              [
     *                  [
     *                      "output",
     *                      10949,
     *                      12300
     *                  ]
     *              ],
     *              isFinished: true,
     *              isStarted: true,
     *              logs: 
     *              [
     *                  [
     *                      "stdio",
     *                      "http://localhost:8010/builders/runtests/builds/3/steps/git/logs/stdio"
     *                  ]
     *              ],
     *              name: "git",
     *              text: 
     *              [
     *                  "update"
     *              ],
     *              times: 
     *              [
     *                  1339336911.227,
     *                  1339336912.597
     *              ]
     *          },
     *          {
     *              expectations: 
     *              [
     *                  [
     *                      "output",
     *                      20354,
     *                      20354
     *                  ]
     *              ],
     *              isFinished: true,
     *              isStarted: true,
     *              logs: 
     *              [
     *                  [
     *                      "stdio",
     *                      "http://localhost:8010/builders/runtests/builds/3/steps/shell/logs/stdio"
     *                  ]
     *              ],
     *              name: "shell",
     *              step_number: 1,
     *              text: 
     *              [
     *                  "'trial",
     *                  "pyflakes'"
     *              ],
     *              times: 
     *              [
     *                  1339336912.598,
     *                  1339336913.861
     *              ]
     *          }
     *      ],
     *      text: 
     *      [
     *          "build",
     *          "successful"
     *      ],
     *      times: 
     *      [
     *          1339336911.227,
     *          1339336913.862
     *      ]
     *  }
     * }
     */

    public class BuildBotBuildStatus : BuildStatus
    {
        protected class DynamicJsonObject : DynamicObject
        {
            private IDictionary<string, object> Dictionary { get; set; }

            public DynamicJsonObject(IDictionary<string, object> dictionary)
            {
                this.Dictionary = dictionary;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var name = binder.Name;
                if (string.Compare(binder.Name, "MinusOne", true) == 0 && this.Dictionary.ContainsKey("-1"))
                {
                    name = "-1";
                }
                else if (string.Compare(binder.Name, "MinusTwo", true) == 0 && this.Dictionary.ContainsKey("-2"))
                {
                    name = "-2";
                }
                result = this.Dictionary[name];
                

                if (result is IDictionary<string, object>)
                {
                    result = new DynamicJsonObject(result as IDictionary<string, object>);
                }
                else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
                {
                    result = new List<DynamicJsonObject>((result as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
                }
                else if (result is ArrayList)
                {
                    result = new List<object>((result as ArrayList).ToArray());
                }

                return this.Dictionary.ContainsKey(name);
            }
        }

        protected class DynamicJsonConverter : JavaScriptConverter
        {
            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                if (dictionary == null)
                    throw new ArgumentNullException("dictionary");

                if (type == typeof(object))
                {
                    return new DynamicJsonObject(dictionary);
                }

                return null;
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
            }
        }

        private static readonly Dictionary<string, BuildStatusInfo> _buildStatusInfo = new Dictionary<string, BuildStatusInfo>();

        private static DateTime FromUnixTime(double _time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(_time).ToLocalTime();
        }
        
        public BuildBotBuildStatus(string _JSONQuery, string _rootURL)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
            dynamic buildStatus = jss.Deserialize<object>(_JSONQuery) as dynamic;
            dynamic mostRecentBuild = buildStatus.MinusOne;

            Name = BuildDefinitionId = mostRecentBuild.builderName;

            BuildStatusInfo buildStatusInfo;
            if (!_buildStatusInfo.TryGetValue(BuildDefinitionId, out buildStatusInfo))
            {
                buildStatusInfo = new BuildStatusInfo();
                _buildStatusInfo.Add(BuildDefinitionId, buildStatusInfo);
            }

            //see if mostRecentBuild is in progress, complete or broken
            bool buildInProgress = true;
            if (mostRecentBuild.times[1] != null)
            {
                FinishedTime = FromUnixTime((double)mostRecentBuild.times[1]);
                
                buildInProgress = false;
            }
            else
            {
                FinishedTime = null;
            }

            StartedTime = FromUnixTime((double)mostRecentBuild.times[0]);

            if (buildInProgress)
            {
                BuildStatusEnum = BuildStatusEnum.InProgress;
            }
            else
            {
                try
                {
                    List<object> text = mostRecentBuild.text;
                    if (text.Contains("successful"))
                    {
                        BuildStatusEnum = BuildStatusEnum.Working;
                    }
                    else if (text.Contains("failed"))
                    {
                        BuildStatusEnum = BuildStatusEnum.Broken;
                    }
                    else
                    {
                        BuildStatusEnum = BuildStatusEnum.Unknown;
                    }
                }
                catch( Exception )
                {
                    BuildStatusEnum = BuildStatusEnum.Unknown;
                }
            }

            try
            {
                List<object> changes = mostRecentBuild.sourceStamp.changes;
                if( changes.Count > 0 && changes[0].GetType() == typeof(Dictionary<String,object>))
                {
                    var change = changes[0] as Dictionary<string, object>;
                    Comment = change["comments"].ToString();
                }
            }
            catch (Exception)
            {
                //drat
            }
            if (Comment == null)
            {
                Comment = mostRecentBuild.reason;
            }

            var requestedby = GetProperty("blamelist", mostRecentBuild);
            if (requestedby != null && requestedby.Count > 1)
            {
                ArrayList fools = requestedby[1];
                var sb = new StringBuilder();
                for( int i = 0; i < fools.Count; ++i )
                {
                    if( i != 0 )
                    {
                        sb.Append(", ");
                    }
                    sb.Append(fools[i].ToString());
                }
                RequestedBy = sb.ToString();
            }
            else
            {
                requestedby = GetProperty("owner", mostRecentBuild);
                if (requestedby != null && requestedby.Count > 1)
                {
                    RequestedBy = requestedby[1];
                }
                else
                {
                    //no idea!
                    RequestedBy = null;
                }
            }
            
            var webUrl = new Uri(_rootURL + "/builders/" + mostRecentBuild.builderName + "/builds/" + mostRecentBuild.number as string);
            Url = webUrl.ToString();
            BuildId = mostRecentBuild.number.ToString();

            buildStatusInfo.LastBuildStatusEnum = BuildStatusEnum;
        }

        private ArrayList GetProperty(string _propertyName, dynamic _buildStatus)
        {
            try
            {
                foreach (ArrayList property in _buildStatus.properties)
                {
                    if (property.Count > 0 && property[0].ToString().Equals(_propertyName))
                    {
                        return property;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private class BuildStatusInfo
        {
            public DateTime? StartedTime { get; set; }
            public DateTime? FinishedTime { get; set; }
            public BuildStatusEnum? LastBuildStatusEnum { get; set; }
        }

        public static void ClearCache()
        {
            _buildStatusInfo.Clear();
        }
    }
}
