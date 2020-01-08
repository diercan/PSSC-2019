//
//  LightViewController.swift
//  FirstApp
//
//  Created by Valentina Vențel on 06/04/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import UIKit

enum Room: Int {
    case hall = 0, living, kitchen
    
    func toString() -> String {
        switch self {
        case .hall:
            return "hall"
        case .living:
            return "living"
        case .kitchen:
            return "kitchen"
        }
    }
    
    func url() -> String {
        switch self {
        case .hall:
            return "updateLight.php"
        case .living:
            return "updateLiving.php"
        case .kitchen:
            return "updateKitchen.php"
        }
    }
}

class LightViewController: UIViewController, LightModelProtocol {
    
    var firstLight: Light = Light()
    var feedItems: NSArray = NSArray()
    let options = ["Hol", "Camera de zi", "Bucătarie"]
   
    @IBOutlet weak var lightTableView: UITableView!
    
    
    func itemsDownloaded(items: NSArray) {
        feedItems = items
        firstLight = feedItems.firstObject as! Light
        lightTableView.reloadData()
        print (" Heiiii \(firstLight.kitchen!)")
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        lightTableView.dataSource = self
        lightTableView.delegate = self
        // Do any additional setup after loading the view.
        title = "Lights"
        lightTableView.tableFooterView = UIView(frame: .zero)
        
        let lightModel = LightModel()
        lightModel.delegate = self
        lightModel.downloadedItems()
        
    }
    
    @objc func switchControl(_ sender: UISwitch) {
        
        let urlPath = "http://127.0.0.1/\(Room(rawValue: sender.tag)!.url())"
            
        guard let url = URL(string: urlPath) else {
            print("Invalid URL", urlPath)
            return
        }
        let session = URLSession(configuration: .default)
            
        var request = URLRequest(url: url)
        request.httpMethod = "POST"
        request.cachePolicy = NSURLRequest.CachePolicy.reloadIgnoringCacheData

        let room = Room(rawValue: sender.tag)?.toString()
        let postString = "\(room!)=\(sender.isOn ? 1:0)"
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
}

extension LightViewController: UITableViewDataSource {
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return options.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = lightTableView.dequeueReusableCell(withIdentifier: "lightCell", for: indexPath) as! LightCell
        cell.textLabel?.text = "\(options[indexPath.row])"
        cell.lightSwitch.tag = indexPath.row
        
    
        cell.lightSwitch.addTarget(self,
                                   action: #selector(switchControl(_:)),
                                   for: .valueChanged)
        
        switch cell.lightSwitch.tag {
        case Room.hall.rawValue:
            do {
                cell.lightSwitch.isOn = (firstLight.hall ?? 0) == 0 ? false : true
                    // ((firstLight.hall!) == 0 ? true:false)
            }
        case Room.living.rawValue:
            do {
                cell.lightSwitch.isOn = (firstLight.living ?? 0) == 0 ? false : true
                // ((firstLight.living!) == 0 ? true:false)
            }
        case Room.kitchen.rawValue:
            do {
                cell.lightSwitch.isOn = (firstLight.kitchen ?? 0) == 0 ? false : true
                    // ((firstLight.kitchen!) == 0 ? true:false)
            }
        default:
            print("Oops! Mai incearca o data!")
        }
        return cell
    }
}

extension LightViewController: UITableViewDelegate {
    
}
