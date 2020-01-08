//
//  HomeModelProtocol.swift
//  FirstApp
//
//  Created by Valentina Vențel on 10/05/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import Foundation

protocol UserModelProtocol: class {
    func itemsDownloaded(items: NSArray)
}

class HomeModel: NSObject, URLSessionDataDelegate {
    weak var delegate: UserModelProtocol!
    let urlPath = "http://127.0.0.1/selectUser.php"
    
    func downloadItems() {
        let url: URL = URL(string: urlPath)!
        let defaultSession = URLSession(configuration: .default)
        let task = defaultSession.dataTask(with: url) { (data, response, error) in
            if error != nil {
                print("Failed to download data")
            } else {
                print("Data downloaded successfully")
                self.parseJSON(data!)
            }
        }
        task.resume()
    }

    fileprivate func parseJSON(_ data: Data) {
        var jsonResult = NSArray()
        
        do {
            jsonResult = try JSONSerialization.jsonObject(with: data, options: JSONSerialization.ReadingOptions.allowFragments) as! NSArray
        } catch let error as NSError {
            print(error)
        }
        
        var jsonElement = NSDictionary()
        let users = NSMutableArray()
        
        for i in 0 ..< jsonResult.count {
            jsonElement = jsonResult[i] as! NSDictionary
            let user = User()
            
            if let userID = jsonElement["UserID"] as? String,
                let password = jsonElement["Password"] as? String,
                let name = jsonElement["Name"] as? String,
                let puk = jsonElement["Imei"] as? String {
                
                user.userID = userID
                user.password = password
                user.name = name
                user.puk = puk
            }
            users.add(user)
        }
        DispatchQueue.main.async(execute: { () -> Void in
            self.delegate.itemsDownloaded(items: users)
        })
    }
}

