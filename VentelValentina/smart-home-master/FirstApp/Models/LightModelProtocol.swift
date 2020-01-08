//
//  LightModelProtocol.swift
//  FirstApp
//
//  Created by Valentina Vențel on 19/05/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import UIKit
import Foundation

protocol LightModelProtocol: class {
    func itemsDownloaded(items: NSArray)
}

class LightModel: NSObject, URLSessionDataDelegate {
    weak var delegate: LightModelProtocol!
    let urlPath = "http://127.0.0.1/selectLights.php"
    
    func downloadedItems() {
        let url: URL = URL(string: urlPath)!
        let defaultSession = Foundation.URLSession(configuration: URLSessionConfiguration.default)
        let task = defaultSession.dataTask(with: url) {
            (data, response, error) in
            if error != nil {
                print ("Failed to downloaded data")
            } else {
                print ("Data downloaded successfully, ieiiiiii")
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
            print (error)
        }
        
        var jsonElement = NSDictionary()
        let lightArray = NSMutableArray()
        
        for i in 0 ..< jsonResult.count {
            jsonElement = jsonResult[i] as! NSDictionary
            let light = Light()
            
            if let str_hall = jsonElement["hall"] as? String, let hall = Int(str_hall),
                let str_living = jsonElement["living_room"] as? String, let living = Int(str_living),
                let str_kitchen = jsonElement["kitchen"] as? String, let kitchen = Int(str_kitchen) {
                light.hall = hall
                light.living = living
                light.kitchen = kitchen
            }
            lightArray.add(light)
        }
        DispatchQueue.main.async(execute: { () -> Void in self.delegate.itemsDownloaded(items: lightArray)})
    }
}
