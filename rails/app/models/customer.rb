class Customer < ApplicationRecord
  def accept_changes(hash)
    attributes.select do |key, value|
      key != :account_number
    end.map do |key, value|
      key
    end.each do |attribute|
      key = attribute.to_s
      if hash.has_key?(key)
        send("#{key}=", hash[key])
      end
    end
  end
end
