class AddColumnsToCustomer < ActiveRecord::Migration
  def change
    change_table :customers do |t|
      t.integer :account_number
      t.string :address_city,
        :address_country,
        :address_street,
        :first_name,
        :gender,
        :last_name,
        :picture_uri
      t.index :account_number
    end
  end
end
