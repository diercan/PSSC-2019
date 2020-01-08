//
//  HTTPClient.swift
//  FirstApp
//
//  Created by Valentina Vențel on 24/05/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import Foundation

class HTTPClient: NSObject {
    class func login(user: String, puk: String, completionHandler: @escaping (_ result: NSArray, _ error: Error?)->()) {
        let urlPath = "http://127.0.0.1/selectUser.php"
        
        let url: URL = URL(string: urlPath)!
        let defaultSession = URLSession(configuration: .default)
        let task = defaultSession.dataTask(with: url) { (data, response, error) in
            if let error = error {
                print("Data task error: " + error.localizedDescription)
            } else if let data = data {
                print("Data downloaded successfully")
                var jsonResult: NSArray = []
                do {
                    jsonResult = try JSONSerialization.jsonObject(with: data,
                        options: JSONSerialization.ReadingOptions.allowFragments) as! NSArray
                } catch let error as NSError {
                    print(error)
                }
                
                completionHandler(jsonResult, error)
            }
        }
        task.resume()
    }
}
