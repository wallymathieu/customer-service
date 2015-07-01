module CustomerServiceHelper
  def element(tag, value)
    if (value == nil)
      return "<#{tag} xsi:nil=\"true\"/>".html_safe
    end
    return "<#{tag}>#{value.to_s.encode(:xml => :text)}</#{tag}>".html_safe
  end
  
  def self.xml_hash_to_ruby_convention(hash)
    return Hash[ hash.map { |key, value| 
      [underscore(key), value_or_nil(value)]
    }]
  end
  private
  def self.underscore(name)
    name.gsub(/::/, '/').
    gsub(/([A-Z]+)([A-Z][a-z])/,'\1_\2').
    gsub(/([a-z\d])([A-Z])/,'\1_\2').
    tr("-", "_").
    downcase
  end
  def self.value_or_nil(value)
    if value.is_a?(Hash) && value["xsi:nil"] == "true"
      return nil
    end
    return value
  end
end
