http://github.com/jrichter/ThermostatMonitor-RubyClient/blob/master/thermo_runner.rb

* Usage
  - Rename api_key.yml.example to api_key.yml.
  - Login to thermostatmonitor.com and find your api key.
  - Paste your api key in the api_key.yml file
  - run
    ruby thermo_runner.rb
  - the script will output info to a log file named tm.log
  *ToDo
  - daemonize thermo_runner, right now you have to run something like
    god or just start a screen session and run ruby thermo_runner.rb
    to get thermo_runner detached from the current shell and in the background

THE SOFTWARE IS PROVIDED ‘AS IS’, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
