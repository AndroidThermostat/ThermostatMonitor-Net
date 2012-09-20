require './thermostat_monitor'
require './log'

tm = ThermostatMonitor.new
log = LogFile.new("tm.log")

tm.loadThermostats

while true do
  begin
    log.puts "checking in - #{Time.now.strftime('%m/%d/%Y - %I:%M%p')}"
    tm.checkin
  rescue => e
    log.puts e.message
  end
  log.puts "outside temp: #{tm.weather.temperature}, inside: #{tm.thermostats.first.temperature}, state: #{tm.thermostats.first.state}" rescue nil
  sleep 60
end
