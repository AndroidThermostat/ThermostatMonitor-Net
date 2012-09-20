class Thermostat
  attr_accessor :name, :ipAddress, :brand, :lastUpdated, :temperature, :state, :successfulUpdate, :error


  def initialize(name, ipAddress, brand)
    @name = name
    @ipAddress = ipAddress
    @brand = brand
    @lastUpdated = Time.now
    @temperature = -999
    @state = ''
    @successfulUpdate = false
    @error = "no errors"
  end

  def update
    case self.brand
    when "RTCOA"
      rtcoaUpdate
    when "RCSTechnology"
      rcstechUpdate
    end
  end

  def to_hash
    hash = {"state" => self.state, "temperature" => self.temperature.to_i, "ipAddress" => self.ipAddress}
  end

  private

  def rtcoaUpdate
    tmData = []
    url = 'http://' + self.ipAddress + '/tstat'
    open(url) do |f|
      f.each_line {|line| tmData << JSON.parse(line)}
    end

    self.temperature = tmData[0]["temp"]
    self.successfulUpdate = true

    case tmData[0]["tstate"]
    when 0
      self.state = "Off"
    when 1
      self.state = "Heat"
    when 2
      self.state = "Cool"
    else
      self.successfulUpdate = false
    end

    self.lastUpdated = Time.now

  rescue  => e
    self.error =  e.message
    self.successfulUpdate = false
  end

  def rcstechUpdate
    # Do stuff for RCSTechnology - don't have one, can't test it
  end

end

class Weather
  attr_accessor :zip, :lastUpdated, :temperature

  def initialize(zip,temperature)
    @zip = zip
    @temperature = temperature
    @lastUpdated = Time.now
  end

  def update(zip,temperature)
    @zip = zip
    @temperature = temperature
    @lastUpdated = Time.now
  end


end
