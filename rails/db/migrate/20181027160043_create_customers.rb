class CreateCustomers < ActiveRecord::Migration[5.2]
  def change
    create_table :customers do |t|
      t.timestamps
      t.integer  "account_number"
      t.string   "address_city"
      t.string   "address_country"
      t.string   "address_street"
      t.string   "first_name"
      t.string   "gender"
      t.string   "last_name"
      t.string   "picture_uri"  
    end
    
    add_index "customers", ["account_number"], name: "index_customers_on_account_number"
  end
end
