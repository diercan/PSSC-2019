//
//  TemperatureViewController.swift
//  FirstApp
//
//  Created by Valentina Vențel on 17/05/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import UIKit
//"localhost","root","","smart_home_db"


class TemperatureViewController: UIViewController, TemperatureModelProtocol {

    @IBOutlet weak var temperatureTextField: UITextField!
    @IBOutlet weak var dateTextField: UITextField!
    @IBOutlet weak var displayTempCell: UITableViewCell!
    private var datePiker: UIDatePicker?
    
    
    @IBAction func uploadData(_ sender: Any) {
        let urlPath = "http://127.0.0.1/insertTemperature.php"
            
        guard let url = URL(string: urlPath) else {
            print("Invalid URL", urlPath)
            return
        }
        let session = URLSession(configuration: .default)
            
        var request = URLRequest(url: url)
        request.httpMethod = "POST"
        request.cachePolicy = NSURLRequest.CachePolicy.reloadIgnoringCacheData
        
        let postString = "item1=\(temperatureTextField.text!)&item2=\(dateTextField.text!)"
        print("Add: \(postString)")
        request.httpBody = postString.data(using: String.Encoding.utf8)

        
        let task = session.dataTask(with: request ) { data, response, error in
            
                guard let _:NSData = data as NSData?, let _:URLResponse = response, error == nil else {
                    print("error")
                    return
                }
                
                let dataString = NSString(data: data!, encoding: String.Encoding.utf8.rawValue)
                print("--------insert----------")
                print(dataString)
                print("------------------------")
        }
        
        
        task.resume()
        
    }

    var feedItems: NSArray = NSArray()
    var selectedTemperature: Temperature = Temperature()
    
    func itemsDownloaded(items: NSArray) {
        feedItems = items
        if feedItems.count == 0 {
            return
        }
        let lastTemperature: Temperature = feedItems.lastObject as! Temperature
        
        displayTempCell.textLabel!.text = "\(lastTemperature.temperature ?? 0.0) ℃"
        displayTempCell.detailTextLabel?.text = "\(lastTemperature.description)"
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        datePiker = UIDatePicker()
        datePiker?.datePickerMode = .date
        datePiker?.addTarget(self, action: #selector(TemperatureViewController.dateChange(datePiker:)), for: .valueChanged)
        let tapGesture = UITapGestureRecognizer(target: self, action: #selector(TemperatureViewController.viewTapped(gestureRecognizer:)))
        view.addGestureRecognizer(tapGesture)
        
        dateTextField.inputView = datePiker
        
        title = "Temperature"
        
        
        let temperatureModel = TemperatureModel()
        temperatureModel.delegate = self
        temperatureModel.downloadedItems()
       
        // Do any additional setup after loading the view.
    }
    
    @objc func viewTapped(gestureRecognizer: UITapGestureRecognizer) {
        view.endEditing(true)
    }
    
    @objc func dateChange(datePiker: UIDatePicker) {
        let dateFormatter = DateFormatter()
        dateFormatter.dateFormat = "MM-dd-yyyy"
        dateTextField.text = dateFormatter.string(from: datePiker.date)
        view.endEditing(true)
    }
   
    override func didReceiveMemoryWarning()
    {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }

}
