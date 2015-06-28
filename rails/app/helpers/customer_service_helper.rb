module CustomerServiceHelper
  def element(tag, value)
    if (value == nil)
      return "<#{tag} xsi:nil=\"true\"/>".html_safe
    end
    return "<#{tag}>#{value.to_s.encode(:xml => :text)}</#{tag}>".html_safe
  end
end
